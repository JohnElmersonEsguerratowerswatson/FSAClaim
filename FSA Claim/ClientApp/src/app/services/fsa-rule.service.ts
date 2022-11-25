import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ITransactClaim } from "./interfaces/ITransactClaim";
import { ITransactClaimResult } from "./interfaces/ITransactClaimResult";

@Injectable({ providedIn: 'root' })
export class FSARuleService {

  private url: string = "";
  constructor(private http: HttpClient, @Inject('FSARULE_URL') private urlFSARule: string) {

  }

  getEmployeeFSARule(employeeID: number): Observable<any> {
    this.url = this.urlFSARule;
    let headers: HttpHeaders = new HttpHeaders();
    headers.append("Accept", "/");
    headers.append("Accept-Encoding", "gzip, deflate, br");
    headers.append("Connection", "keep-alive");
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let body = { employeeID: employeeID };
    let options = {
      headers: headers
    };

    return this.http.get<any>(this.url + '?args=' + employeeID);
    //.pipe(tap(data => console.log("All", JSON.stringify(data))))
    /// .pipe(catchError(this.handleError));

    //let headers: Headers = new Headers();
    //headers.append("Authorization", "Bearer " + bearer)
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
