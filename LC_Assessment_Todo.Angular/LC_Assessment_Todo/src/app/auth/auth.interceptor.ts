import { HttpEvent, HttpEventType, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

export function authInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
    const token = sessionStorage.getItem("token");
    const router = inject(Router);

    /// If there is token, return request with headers updated.
    if (token) {

      const jwtPayload = JSON.parse(window.atob(token.split('.')[1]))
      const isExpired = Date.now() >= jwtPayload.exp * 1000;
      if(isExpired) router.navigate(['/logout']);

      const reqWithHeader = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token),
      });
      return next(reqWithHeader);
    }

    // If there is no token, return request as it was.
    return next(req);
}
