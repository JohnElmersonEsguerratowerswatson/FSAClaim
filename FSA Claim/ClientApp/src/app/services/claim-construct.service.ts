import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ITransactClaimResult } from "./interfaces/ITransactClaimResult";

@Injectable()
export class ClaimConstructService {

  constructor(private http: HttpClient, @Inject('CLAIMADD_URL') private urlAdd: string, @Inject('CLAIMEDIT_URL') private urlEdit: string) {

  }

  getClaimDetailsAPI(): Observable<ITransactClaimResult> {
    let url: string = "";
    return this.http.get<ITransactClaimResult>(url)
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
