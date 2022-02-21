import { HttpClient } from '@angular/common/http';
import { AccountService } from '../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  //loggedIn: boolean = false;
  //currentUser$: Observable<User>;

  constructor(public accountSrv: AccountService ) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    //this.currentUser$ = this.accountSrv.currentUser$;
  }

  login(){
    // deprecated srubscribe
    // this.accountSrv.login(this.model).subscribe(response => {
    //   console.log(response);
    //   this.loggedIn = true;
    // }, error => {
    //   console.log(error);
    // });

    this.accountSrv.login(this.model).subscribe({
      next: (v) => {
        console.log(v);
       // this.loggedIn = true;
      },
      error: (e) => {console.log(e);
        complete: () => {console.log('Complete');
        }
      }
    });
  }

  logout(){
    //this.loggedIn = false;
    //console.log(this.loggedIn);
    this.accountSrv.logout();
    
  }

  // getCurrentUser(){
  //   this.accountSrv.currentUser$.subscribe({
  //     next: (user) => {
  //       this.loggedIn = !!user;
  //     },
  //     error: error => {
  //       console.log(error);        
  //     }
  //   });
  // }

}
