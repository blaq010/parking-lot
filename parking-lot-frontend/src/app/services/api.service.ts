import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private backendUrl = 'https://localhost:7037/api/parking/parking-lots'; // Replace with your backend API URL

  constructor(private http: HttpClient) { }

  public getData() {
    return this.http.get(`${this.backendUrl}/api/data`);
  }

  public postData(data: any) {
    return this.http.post(`${this.backendUrl}/api/data`, data);
  }
  
  // Add more methods for other API operations as needed
}
