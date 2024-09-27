import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';
import { Task } from '../models/task.model';

@Component({
  selector: 'app-checklist-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checklist-view.component.html',
  styleUrl: './checklist-view.component.scss'
})
export class ChecklistViewComponent{
  private taskService = inject(TaskService);

  tasks = this.taskService.tasks;
  isSyncing = this.taskService.isSyncing;

  onTaskStatusChanged(values:any, task:Task):void{
    this.taskService.changeTaskStatus(task, values.currentTarget.checked);
  }
}
