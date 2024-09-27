import { HttpClient } from "@angular/common/http";
import { inject, Injectable, signal} from "@angular/core";
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
  // private _isLoggedIn: boolean = false;
  public isLoggedIn = signal<boolean>(false);

  login(login: Login):Observable<Result<AuthToken>> {
    return this.http.post<Result<AuthToken>>(this.serviceUrl + '/login', login)
      .pipe(
        tap(p => {
          let token = "" + p.data?.token;
          this.token = token;
          this.setLoggedIn();
        }),
        catchError(
          err => of(({ data: {}, error:this.errorService.formatError(err) } as Result<AuthToken>))
        )
      );
  }

  logout(){
    this.isLoggedIn.update(x => x = false);
    sessionStorage.clear();
  }

  private setLoggedIn():void{
    this.isLoggedIn.update(x => x=true);
  }
  // get isLoggedIn():boolean{
  //   return this.isLoggedIn;
  // }

  set token(value:string) {
    sessionStorage.setItem('token', value);
  }

  get token() {

    return "" + sessionStorage?.getItem('token');
  }


}
