import { HttpClient } from "@angular/common/http";
import { computed, inject, Injectable, signal } from "@angular/core";
import { Result } from "../models/result.model";
import { Task } from "../models/task.model";
import { catchError, combineLatest, Observable, of, shareReplay, switchMap, tap } from "rxjs";
import { HttpErrorService } from "./http-error.service";
import { toSignal } from "@angular/core/rxjs-interop";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private tasksUrl = 'https://localhost:7114/task';
  private http = inject(HttpClient);
  private errorService = inject(HttpErrorService)

  private tasksResult$ = this.http.get<Result<Task[]>>(this.tasksUrl + '/list')
  .pipe(
    tap(p => console.log(JSON.stringify(p))),
    catchError(
      err => of(({ data: [], error:this.errorService.formatError(err) } as Result<Task[]>))
    )
  );

  private tasksResult = toSignal(this.tasksResult$,
    {initialValue: {data: []} as Result<Task[]>});

  public tasks = computed(() => this.tasksResult().data);
  public tasksError = computed(()=> this.tasksResult().error);
  public isSyncing = signal<boolean>(false);


  changeTaskTitle(task: Task) {
    this.updateTask(task);
  }

  changeTaskStatus(task: Task, checked: boolean) {
    task.isDone = checked;
    this.updateTask(task);
  }

  private updateTask(task: Task):void {
    this.setSyncingStateOn();
    this.http.put<Result<Task>>(this.tasksUrl, task)
      .pipe(
        tap(p => {
          console.log("Update task response: " + JSON.stringify(p));
          this.setSyncingStateOff();
        }),
        catchError(
          err => of(({ data: [], error:this.errorService.formatError(err) } as Result<Task[]>))
        )
      )
      .subscribe();
  }

  public createTask(task: Task): Observable<Result<Task>> {
    this.setSyncingStateOn();
    return this.http.post<Result<Task>>(this.tasksUrl, task)
      .pipe(
        tap(p => {
          console.log("Create task response: " + JSON.stringify(p));
          this.setSyncingStateOff();
        }),
        catchError(
          err => of(({ data: {}, error:this.errorService.formatError(err) } as Result<Task>))
        )
      );
  }

  private setSyncingStateOn(): void {
    this.isSyncing.update(x => x = true);
  }

  private setSyncingStateOff(): void {
    this.isSyncing.update(x => x = false);
  }
}
