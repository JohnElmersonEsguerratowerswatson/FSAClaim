import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ITransactClaim } from "./interfaces/ITransactClaim";
import { ITransactClaimResult } from "./interfaces/ITransactClaimResult";

@Injectable({ providedIn: 'root' })
export class ClaimDeleteService {
   
                                      //@Inject('CLAIMDETAIL_URL') private url: string
  constructor(private http: HttpClient, @Inject('CLAIMDELETE_URL') private url: string) {

  }

  claimDeleteAPI(claim: ITransactClaim): Observable<any> {
    this.url = this.url;
    let headers: HttpHeaders = new HttpHeaders();
    headers.append("Accept", "/");
    headers.append("Accept-Encoding", "gzip, deflate, br");
    headers.append("Connection", "keep-alive");
    headers.append("Content-Type", "application/x-www-form-urlencoded");

    let options = {
      headers: headers
    };


    return this.http.post<any>(this.url, claim, options);

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
