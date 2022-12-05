import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { IFSARule } from "./interfaces/IFSARule";


@Injectable({ providedIn: 'root' })
export class FSARuleService {

  private url: string = "";
  constructor(private http: HttpClient, @Inject('FSARULE_URL') private urlFSARule: string) {

  }

  addEmployeeFSARule(fsaRule: IFSARule): Observable<any> {
    this.url = this.urlFSARule; console.log(this.url);
    let headers: HttpHeaders = new HttpHeaders();
    headers.append("Accept", "/");
    headers.append("Accept-Encoding", "gzip, deflate, br");
    headers.append("Connection", "keep-alive");
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let body = fsaRule;
    let options = {
      headers: headers
    };

    return this.http.post<any>(this.url,body,options);

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
