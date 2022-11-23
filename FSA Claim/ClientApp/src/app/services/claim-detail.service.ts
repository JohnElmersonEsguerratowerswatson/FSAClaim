import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { IViewClaim } from "./interfaces/IViewClaim";

@Injectable()
export class ClaimDetailService {

  constructor(private http: HttpClient, @Inject('CLAIMDETAIL_URL') private url: string) {

  }

  getClaimDetailsAPI(): Observable<IViewClaim> {
    return this.http.get<IViewClaim>(this.url)
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
