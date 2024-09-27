import { HttpClient } from "@angular/common/http";
import { computed, inject, Injectable } from "@angular/core";
import { Result } from "../models/result.model";
import { Task } from "../models/task.model";
import { catchError, map, of, shareReplay, tap } from "rxjs";
import { HttpErrorService } from "./http-error.service";
import { toSignal } from "@angular/core/rxjs-interop";

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private tasksUrl = 'https://localhost:7114/task/list';
  private http = inject(HttpClient);
  private errorService = inject(HttpErrorService)

  private tasksResult$ = this.http.get<Result<Task[]>>(this.tasksUrl)
  .pipe(
    // map(p => ({ data:p.data} as Result<Task[]>)),
    tap(p => console.log(JSON.stringify(p))),
    shareReplay(1),
    catchError(
      err => of(({ data: [], error:this.errorService.formatError(err) } as Result<Task[]>))
    )
  );

  private tasksResult = toSignal(this.tasksResult$,
    {initialValue: {data: []} as Result<Task[]>});

  public tasks = computed(() => this.tasksResult().data);
  public tasksError = computed(()=> this.tasksResult().error);

}
