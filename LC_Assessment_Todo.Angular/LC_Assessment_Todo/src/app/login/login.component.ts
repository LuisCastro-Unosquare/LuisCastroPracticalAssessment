import { Component, inject, OnDestroy } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { Login } from '../models/login.model';
import { EMPTY, Observable, of, Subscription, tap } from 'rxjs';
import { AuthToken } from '../models/authToken';
import { Result } from '../models/result.model';

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

  onLoginSubmit():void {
    this.login = this.loginService.login(<Login>{username: 'Luis.Castro', password: 'Pass.word'}).subscribe();
  }

  ngOnDestroy(): void {
    this.login.unsubscribe();
  }
}
