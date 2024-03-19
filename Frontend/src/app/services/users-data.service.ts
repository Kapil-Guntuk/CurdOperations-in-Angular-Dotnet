import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsersDataService {

  baseUrl = 'https://localhost:44394/api/values';

  constructor(private http: HttpClient) { }
  getAllUsers() {
    return this.http.get(this.baseUrl);
  }

  addNewUser(data: any) {
    return this.http.post(this.baseUrl, data);
  }

  updateUser(userId: number, userData: any) {
    return this.http.put(`${this.baseUrl}/${userId}`, userData);
  }

  getUserById(userId: number){
    return this.http.get(`${this.baseUrl}/${userId}`);
  }

  deleteUser(userId: number) {
    return this.http.delete(`${this.baseUrl}/${userId}`);
  }
}
