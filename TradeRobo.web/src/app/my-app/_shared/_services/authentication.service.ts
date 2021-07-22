import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BaseService } from './base.service';
import { ToastrService } from 'ngx-toastr';
import { EnvConfigService } from '.';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })

export class AuthenticationService extends BaseService {
    private currentUserSubject: BehaviorSubject<ApiModel.User>;
    public currentUser: Observable<ApiModel.User>;


  constructor(private httpClient: HttpClient, toastrService: ToastrService, configService: EnvConfigService, private router: Router) { 
      super(toastrService, configService); 
      this.currentUserSubject = new BehaviorSubject<ApiModel.User>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): ApiModel.User {
        return this.currentUserSubject.value;
    }

    public authenticate(data) {
        return this.httpClient.post<any>(this.getUrl('/user/authenticate'), data).pipe(map(user => {
            // store user details and jwt token in local storage to keep user logged in between page refreshes
            localStorage.setItem('currentUser', JSON.stringify(user));
            this.currentUserSubject.next(user);
            return user;
        }));
      }
    
      logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        location.reload(true);
        //this.router.navigate(['/trade/login']);
    }
}
