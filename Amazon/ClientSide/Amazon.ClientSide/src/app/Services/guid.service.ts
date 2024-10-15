// src/app/services/guid.service.ts
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';   
import { v4 as uuidv4 } from 'uuid';  

@Injectable({
  providedIn: 'root'
})
export class GuidService {

  private readonly cookieName = 'guid';  

  constructor(private cookieService: CookieService) { }

  initializeGUID() {
    let guid = this.cookieService.get(this.cookieName); 

    if (!guid) {
      guid = uuidv4();
      this.cookieService.set(this.cookieName, guid, { expires: 7, path: '/' }); 
      console.log('New GUID created and stored:', guid);
    } else {
      console.log('Existing GUID found:', guid);
    }

    return guid;
  }

  getGUID(): string {
    const guid = this.cookieService.get(this.cookieName);
    console.log('Retrieved GUID:', guid);
    return guid;
  }

  deleteGUID() {
    this.cookieService.delete(this.cookieName, '/');
    console.log('GUID deleted from cookie');
  }
}
