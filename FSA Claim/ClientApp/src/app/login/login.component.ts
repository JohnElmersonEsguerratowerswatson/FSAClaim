import { state } from "@angular/animations";
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
  public showError: boolean = false;

  constructor(private loginService: LoginService, private router: Router) {
    this.loginModel = {} as ILogin;
//alert(this.loginService.bearer);
  }

  ngOnInit(): void {


  }

  login(): void {
    //  let loginResult = ;
    this.showError = false;

    this.subscription = this.loginService.login(this.loginModel.Username, this.loginModel.Password).subscribe(
      loginResult => {
        this.routToHome();
      },
      errorResult => {
        this.showErrorMessage();
      }
    );
  }

  routToHome(): void {
    this.router.navigateByUrl("/claim-list", { state: this.loginService.bearer });
  }

  showErrorMessage(): void {
    this.showError = true;
  }

  ngOnDestroy(): void {
    //this.subscription?.unsubscribe();
  }

}
