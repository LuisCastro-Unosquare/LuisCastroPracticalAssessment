import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export function authInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
    const token = sessionStorage.getItem("token");

    /// If there is token, return request with headers updated.
    if (token) {
      const reqWithHeader = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token),
      });
      return next(reqWithHeader);
    }

    // If there is no token, return request as it was.
    return next(req);
}
