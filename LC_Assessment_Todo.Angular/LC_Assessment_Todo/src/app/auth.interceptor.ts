import { HttpEvent, HttpEventType, HttpHandlerFn, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, tap } from 'rxjs';

export function authInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn): Observable<HttpEvent<unknown>> {
    // TODO: Create authService with localStorage for token then retrieve token here from localStorage
    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE3MjczOTU5NTksImV4cCI6MTcyNzQwNDk1OSwiaWF0IjoxNzI3Mzk1OTU5fQ.vfQIrrkRIt-MsUH7E6xNFmctj8iC7cJ2EYEM3Kz8N4A';
    const reqWithHeader = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + token),
    });

    return next(reqWithHeader);
    // .pipe(
    //   tap(event => {
    //     // if (event.type === HttpEventType.Response) {
    //       // console.log(req.url, 'returned a response with status', event.status);
    //       console.log("LC you did it", reqWithHeader);
    //     // }
    //   }));
}
