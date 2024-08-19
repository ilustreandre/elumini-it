import { Component, OnInit } from '@angular/core';
import { UserTaskService } from '../services/usertask.service';
import { UserTask } from '../models/userTask';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  userTasks = [] as UserTask[];

  formData = {
    description: '',
    insertDate: '',
    status: 0
  }

  constructor(private userTaskService: UserTaskService) { }

  ngOnInit() {
    this.getUserTasks();
  }

  saveUserTask() {
    var snapshot: any = {
      description: this.formData.description,
      insertDate: new Date(),
      status: Number(this.formData.status)
    }

    this.userTaskService.saveUserTask(snapshot);

    this.formData.description = '';
    this.formData.status = 0;    
  }

  reload() {
    this.getUserTasks();
  }

  getUserTasks() {
    this.userTaskService.getUserTasks().subscribe((userTasks: UserTask[]) => {
      this.userTasks = userTasks;
    });
  }
}
