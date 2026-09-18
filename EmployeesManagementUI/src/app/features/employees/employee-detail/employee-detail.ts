import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Employee } from '../../../core/models/employee.model';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-employee-detail',
  imports: [RouterLink, DatePipe],
  templateUrl: './employee-detail.html',
})
export class EmployeeDetail {
  private readonly service = inject(EmployeeService);
  private readonly route = inject(ActivatedRoute);

  protected readonly employee = signal<Employee | null>(null);
  protected readonly error = signal<string | null>(null);

  constructor() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.service.getById(id).subscribe({
        next: (employee) => this.employee.set(employee),
        error: () => this.error.set('Unable to load the employee.'),
      });
    }
  }
}
