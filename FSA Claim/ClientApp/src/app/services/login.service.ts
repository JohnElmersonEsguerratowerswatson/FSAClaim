import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ILogin } from "./interfaces/ILogin";
import { ILoginResult } from "./interfaces/ILoginResult";


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  public bearer: any;

  constructor(private http: HttpClient, @Inject('LOGIN_URL') private url: string) {

  }

  login(username: string, password: string): Observable<ILoginResult> {
    return this.http.post<ILoginResult>(this.url, { Username: username, Password: password } as ILogin)
      //.pipe(tap(data => { this.bearer = data.bearer; console.log("All", this.bearer); }))
      .pipe(catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = "";
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    }
    else {
      errorMessage = `Server returned code: ${err.status},error message is ${err.message}.`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }

}
