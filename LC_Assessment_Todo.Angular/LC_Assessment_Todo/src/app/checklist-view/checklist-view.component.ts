import { Component, inject } from '@angular/core';
import { TaskService } from '../services/task.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-checklist-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './checklist-view.component.html',
  styleUrl: './checklist-view.component.scss'
})
export class ChecklistViewComponent {
  private taskService = inject(TaskService);

  tasks = this.taskService.tasks;
}
