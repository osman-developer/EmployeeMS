import { Component } from '@angular/core';
import { EmployeeDashboardComponent } from './core/components/employee-dashboard/employee-dashboard.component';

@Component({
  selector: 'app-root',
  imports: [EmployeeDashboardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'EmployeeMSClient';
}
