import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  /**
   *
   */
  constructor(private accSrv: AccountService, private toastr: ToastrService) {
    
    
  }
  canActivate(): Observable<boolean> {
    return this.accSrv.currentUser$.pipe(
      map(user => {
        if(user) return true;
        this.toastr.error('You shall not pass!');
      })
    );
  }
  
}
