import { Component, inject, OnDestroy } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Login } from '../models/login.model';
import { Subscription, tap } from 'rxjs';

import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnDestroy {

  private loginService = inject(AuthService);
  private login!: Subscription;
  private router = inject(Router);

  onLoginSubmit():void {
    this.login = this.loginService.login(<Login>{username: 'Luis.Castro', password: 'Pass.word'})
      .pipe(
        tap(x => this.router.navigate(['/landingpage']))
      )
      .subscribe();
  }

  ngOnDestroy(): void {
    this.login?.unsubscribe();
  }
}
