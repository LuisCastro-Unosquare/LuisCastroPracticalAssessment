import { Routes } from '@angular/router';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

export const routes: Routes = [
  { path: 'landingpage', component: LandingPageComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },

];
