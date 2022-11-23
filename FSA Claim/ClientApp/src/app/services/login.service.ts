import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

import { catchError, Observable, tap, throwError } from "rxjs";
import { ILogin } from "./interfaces/ILogin";
import { ILoginResult } from "./interfaces/ILoginResult";


@Injectable({
  providedIn:'root'
})
export class LoginService {


  constructor(private http: HttpClient, @Inject('LOGIN_URL') private url: string) {

  }

  login(username: string, password: string): Observable<string> {
    return this.http.post<string>(this.url, { Username : username, Password : password } as ILogin)
      .pipe(tap(data => console.log("All", JSON.stringify(data))))
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
