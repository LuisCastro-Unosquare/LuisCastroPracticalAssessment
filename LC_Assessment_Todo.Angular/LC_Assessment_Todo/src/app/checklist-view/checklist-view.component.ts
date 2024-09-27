import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';
import { Task } from '../models/task.model';
import { Subscription, tap } from 'rxjs';

@Component({
  selector: 'app-checklist-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checklist-view.component.html',
  styleUrl: './checklist-view.component.scss'
})
export class ChecklistViewComponent implements OnInit, OnDestroy{
  private taskService = inject(TaskService);
  newTask:Task = <Task>{title:""};
  createNewTaskSub!: Subscription;
  deleteTaskSub!: Subscription;

  tasks = this.taskService.tasks;
  isSyncing = this.taskService.isSyncing;

  ngOnInit(): void {
    this.taskService.getAllTasks();
  }

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
        })
      ).subscribe();

      event.currentTarget.blur();
  }

  onDeleteTask(event: any, task: Task):void{
    this.deleteTaskSub = this.taskService.deleteTask(task.id).subscribe();
  }

  ngOnDestroy(): void {
    if(this.createNewTaskSub) this.createNewTaskSub.unsubscribe();
    if(this.deleteTaskSub) this.deleteTaskSub.unsubscribe();
  }
}
