import { Component, inject } from '@angular/core';
import { TaskService } from '../services/task.service';

@Component({
  selector: 'app-checklist-view',
  standalone: true,
  imports: [],
  templateUrl: './checklist-view.component.html',
  styleUrl: './checklist-view.component.scss'
})
export class ChecklistViewComponent {
  private taskService = inject(TaskService);
}
