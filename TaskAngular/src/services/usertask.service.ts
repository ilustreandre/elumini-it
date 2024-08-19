import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { UserTask } from '../models/userTask';

@Injectable({
  providedIn: 'root'
})
export class UserTaskService {
  url = 'http://localhost:5230/api/UserTask';

  constructor(private httpClient: HttpClient) { }
  
  getUserTasks(): Observable<UserTask[]> {
    return this.httpClient.get<UserTask[]>(this.url)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }
  
  saveUserTask(userTask: UserTask) {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(userTask);
    console.log(body)
    console.log(this.url)

    this.httpClient.post(this.url, body, { 'headers': headers })
      .pipe(
        retry(2),
        catchError(this.handleError))
      .subscribe()
  }
  
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {      
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error code: ${error.status}, ` + `message: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };
}
