import { Component } from '@angular/core';
import { ChecklistViewComponent } from "../checklist-view/checklist-view.component";

@Component({
  selector: 'app-landing-page',
  standalone: true,
  imports: [ChecklistViewComponent],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss'
})
export class LandingPageComponent {

}
