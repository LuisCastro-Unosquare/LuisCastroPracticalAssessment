import { HttpClient } from "@angular/common/http";
import { inject, Injectable} from "@angular/core";
import { Result } from "../models/result.model";
import { catchError, Observable, of, shareReplay, tap } from "rxjs";
import { AuthToken } from "../models/authToken";
import { Login } from "../models/login.model";
import { HttpErrorService } from "../services/http-error.service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private serviceUrl = 'https://localhost:7114/auth';
  private http = inject(HttpClient);
  private errorService = inject(HttpErrorService)

  login(login: Login):Observable<Result<AuthToken>> {
    return this.http.post<Result<AuthToken>>(this.serviceUrl + '/login', login)
      .pipe(
        tap(p => {
          let token = "" + p.data?.token;
          this.token = token;
        }),
        catchError(
          err => of(({ data: {}, error:this.errorService.formatError(err) } as Result<AuthToken>))
        )
      );
  }

  set token(value:string) {
    sessionStorage.setItem('token', value);
  }

  get token() {
    return "" + sessionStorage?.getItem('token');
  }
}
