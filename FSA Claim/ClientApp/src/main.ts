import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';


export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}


export function getClaimsUrl() {
  return 'https://localhost:7254/api/Claims/GetList';
}
export function getClaimDetailsUrl() {
  return 'https://localhost:7254/api/Claims/Details?arg=';
}
export function getClaimEditUrl() {
  return 'https://localhost:7254/api/Claims/Edit';
}
export function getClaimDeleteUrl() {
  return 'https://localhost:7254/api/Claims/Delete';
}
export function getClaimAddUrl() {
  return 'https://localhost:7254/api/Claims/Create';
}
export function getLoginUrl() {
  return 'https://localhost:7254/api/Login';
}
export function getFSARuleURL() {
  return 'https://localhost:7254/api/FSARule/Add';
}

export function getFSAClaimsForApprovalURL() {
  return 'https://localhost:7254/api/FSAClaimAdministration/Index';
}

export function getFSAClaimAapprovalURL() {
  return 'https://localhost:7254/api/FSAClaimAdministration/ClaimApproval';
}

export function getEmployeeListUrl() {
  return 'https://localhost:7254/api/Employee/Index';
}

  const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
    ,
    { provide: 'CLAIMDETAIL_URL', useFactory: getClaimDetailsUrl, deps: [] },
    { provide: 'CLAIMLIST_URL', useFactory: getClaimsUrl, deps: [] },
    { provide: 'CLAIMEDIT_URL', useFactory: getClaimEditUrl, deps: [] },
    { provide: 'CLAIMDELETE_URL', useFactory: getClaimDeleteUrl, deps: [] },
    { provide: 'CLAIMADD_URL', useFactory: getClaimAddUrl, deps: [] },
    { provide: 'LOGIN_URL', useFactory: getLoginUrl, deps: [] },
    { provide: 'FSARULE_URL', useFactory: getFSARuleURL, deps: [] },
    { provide: 'FSAFORAPPROVALLIST_URL', useFactory: getFSAClaimsForApprovalURL, deps: [] },
    { provide: 'FSAAPPROVAL_URL', useFactory: getFSAClaimAapprovalURL, deps: [] },
    { provide: 'EMPLOYEELIST_URL', useFactory: getEmployeeListUrl, deps: [] }
  ];

  if (environment.production) {
    enableProdMode();
  }

  platformBrowserDynamic(providers).bootstrapModule(AppModule)
    .catch(err => console.log(err));
