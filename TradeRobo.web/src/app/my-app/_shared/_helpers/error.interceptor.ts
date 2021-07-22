import { AuthenticationService, ApiService } from 'app/my-app/_shared';
import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private service: AuthenticationService, private apiService: ApiService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if (err.status === 401) {
                // auto logout if 401 response returned from api
                this.service.logout();
                // location.reload(true);
            }

            // const error = err.error.message || err.statusText;
            this.apiService.setError(err.error);
            console.log(err);
            return throwError(err);
        }));
    }
}
