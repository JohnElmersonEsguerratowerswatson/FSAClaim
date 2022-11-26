import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ClaimApproval } from "./data-classes/ClaimApproval";
import { ITransactClaim } from "./interfaces/ITransactClaim";
import { ITransactClaimResult } from "./interfaces/ITransactClaimResult";

@Injectable({ providedIn: 'root' })
export class ClaimApprovalService {

  private url: string = "";
  constructor(private http: HttpClient, @Inject('FSAAPPROVAL_URL') private urlClaimApproval: string, @Inject('FSAFORAPPROVALLIST_URL') private urlGetList: string) {

  }

  claimApprovalAPI(claimApproval: ClaimApproval): Observable<any> {
    this.url = this.urlClaimApproval;
    let headers: HttpHeaders = new HttpHeaders();
    headers.append("Accept", "/");
    headers.append("Accept-Encoding", "gzip, deflate, br");
    headers.append("Connection", "keep-alive");
    headers.append("Content-Type", "application/x-www-form-urlencoded");

    let options = {
      headers: headers
    };
    alert(claimApproval.referenceNumber);
    return this.http.post<any>(this.url, claimApproval, options);
  }


  claimTableAPI(): Observable<any> {
    this.url = this.urlGetList;
    let headers: HttpHeaders = new HttpHeaders();
    headers.append("Accept", "/");
    headers.append("Accept-Encoding", "gzip, deflate, br");
    headers.append("Connection", "keep-alive");
    headers.append("Content-Type", "application/x-www-form-urlencoded");

    let options = {
      headers: headers
    };

    return this.http.get<any>(this.url);
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
