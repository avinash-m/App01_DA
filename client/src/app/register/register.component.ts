import { AccountService } from './../_services/account.service';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() usersFromHomeComponent: any;
  @Output() cancelRegister = new EventEmitter();

  model: any = {}
  users: any;

  constructor(private http:HttpClient, private accSrv:AccountService) { }

  ngOnInit(): void {
  }

  ID: number = 999999;
  register(){
    console.log(this.model);

    this.accSrv.register(this.model).subscribe({
      next: (x: any)=> {
        this.ID = x.ID;
        console.log(x);
        this.cancel();
      },
      error: e => console.log(e)      
    });
    
  }

  cancel(){
    console.log('canceled');
    
    this.cancelRegister.emit(false);
  }
  

}
