import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: 'landingpage', component: LandingPageComponent },
  { path: 'login', component: LoginComponent },
];
