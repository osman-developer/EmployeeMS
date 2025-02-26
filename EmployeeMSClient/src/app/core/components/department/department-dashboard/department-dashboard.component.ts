import { Component, OnInit } from '@angular/core';
import { DepartmentService } from '../../../_services/departmentAPI.service';

@Component({
  selector: 'app-department-dashboard',
  standalone: false,
  templateUrl: './department-dashboard.component.html',
  styleUrl: './department-dashboard.component.css',
})
export class DepartmentDashboardComponent implements OnInit {
  constructor(private _departmentService: DepartmentService) {}

  ngOnInit(): void {}
}
