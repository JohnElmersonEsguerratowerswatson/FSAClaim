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



  const providers = [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }
    ,
    { provide: 'CLAIMDETAIL_URL', useFactory: getClaimDetailsUrl, deps: [] },
    { provide: 'CLAIMLIST_URL', useFactory: getClaimsUrl, deps: [] },
    { provide: 'CLAIMEDIT_URL', useFactory: getClaimEditUrl, deps: [] },
    { provide: 'CLAIMDELETE_URL', useFactory: getClaimDeleteUrl, deps: [] },
    { provide: 'CLAIMADD_URL', useFactory: getClaimAddUrl, deps: [] },
    { provide: 'LOGIN_URL', useFactory: getLoginUrl, deps: [] }
  ];

  if (environment.production) {
    enableProdMode();
  }

  platformBrowserDynamic(providers).bootstrapModule(AppModule)
    .catch(err => console.log(err));
