import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';
import { Task } from '../models/task.model';
import { Subscription, tap } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-checklist-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checklist-view.component.html',
  styleUrl: './checklist-view.component.scss'
})
export class ChecklistViewComponent implements OnDestroy{
  private taskService = inject(TaskService);
  private router = inject(Router);
  newTask:Task = <Task>{title:""};
  createNewTaskSub!: Subscription;

  tasks = this.taskService.tasks;
  isSyncing = this.taskService.isSyncing;

  onTaskStatusChanged(values:any, task:Task):void{
    this.taskService.changeTaskStatus(task, values.currentTarget.checked);
  }

  onTaskTitleChange(event: any, task: Task) {
    let currentValue = event.currentTarget.value;
    let currentTask = {...task};
    currentTask.title = currentValue;
    this.taskService.changeTaskTitle(currentTask);
    event.currentTarget.blur();
  }

  onNewTask(event: any){
    let newTitleCurrentValue = event.currentTarget.value;
    if(!newTitleCurrentValue) return;

    this.newTask.title = newTitleCurrentValue;
    this.createNewTaskSub = this.taskService.createTask(this.newTask)
      .pipe(
        tap(x=> {
          this.newTask.title = "";

          // Work around, implement behaviorSubject with signal suggested by Deborah Kurata -> https://www.reddit.com/r/Angular2/comments/1d6zcy3/comment/l6wobyp/?utm_source=share&utm_medium=web3x&utm_name=web3xcss&utm_term=1&utm_content=share_button
          window.location.reload();
        })
      ).subscribe();

      event.currentTarget.blur();
  }

  ngOnDestroy(): void {
    if(this.createNewTaskSub) this.createNewTaskSub.unsubscribe();
  }
}
