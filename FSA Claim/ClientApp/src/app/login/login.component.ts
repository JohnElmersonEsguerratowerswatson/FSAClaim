import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";
import { ILogin } from "../services/interfaces/ILogin";
import { LoginService } from "../services/login.service";


@Component({
  selector: 'app-login-component',
  templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit, OnDestroy {

  public loginModel: ILogin;
  private subscription: Subscription | undefined;

  constructor(private loginService: LoginService) {
    this.loginModel = {} as ILogin;
  }

  ngOnInit(): void {


  }


  login() {
    this.subscription = this.loginService.login(this.loginModel.Username, this.loginModel.Password).subscribe();
    alert("sdf");
  }


  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

}
