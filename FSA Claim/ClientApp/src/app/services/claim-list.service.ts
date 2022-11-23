import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { IViewClaim } from "./interfaces/IViewClaim";

@Injectable()
export class ClaimListService {


  constructor(private http: HttpClient, @Inject('CLAIMLIST_URL') private url: string) {

  }

  getClaimsAPI(): Observable<IViewClaim[]> {
    return this.http.get<IViewClaim[]>(this.url)
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
