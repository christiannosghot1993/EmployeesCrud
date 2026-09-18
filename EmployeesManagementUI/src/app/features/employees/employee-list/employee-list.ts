import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Employee } from '../../../core/models/employee.model';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-employee-list',
  imports: [RouterLink, DatePipe],
  templateUrl: './employee-list.html',
})
export class EmployeeList {
  private readonly service = inject(EmployeeService);

  protected readonly employees = signal<Employee[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal<string | null>(null);

  constructor() {
    this.load();
  }

  protected load(): void {
    this.loading.set(true);
    this.error.set(null);
    this.service.getAll().subscribe({
      next: (data) => {
        this.employees.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Unable to load employees.');
        this.loading.set(false);
      },
    });
  }

  protected remove(employee: Employee): void {
    if (!confirm(`Delete ${employee.firstName} ${employee.lastName}?`)) {
      return;
    }

    this.service.delete(employee.id).subscribe({
      next: () => this.employees.update((list) => list.filter((e) => e.id !== employee.id)),
      error: () => this.error.set('Unable to delete employee.'),
    });
  }
}
