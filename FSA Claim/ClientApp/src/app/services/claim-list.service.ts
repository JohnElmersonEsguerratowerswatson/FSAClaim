import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { ViewClaim } from "../data-classes/ViewClaim";
import { IGetClaimResult } from "./interfaces/IGetClaimsResult";
import { IViewClaim } from "./interfaces/IViewClaim";

@Injectable({ providedIn: 'root' })
export class ClaimListService {


  constructor(private http: HttpClient, @Inject('CLAIMLIST_URL') private url: string) {

  }

  getClaimsAPI(): Observable<IGetClaimResult> {
    //let headers: Headers = new Headers();
    //headers.append("Authorization", "Bearer " + bearer)

    return this.http.get<IGetClaimResult>(this.url)
      
      .pipe(catchError(this.handleError));
    //.pipe(tap(data => console.log("All", JSON.stringify(data))))
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
