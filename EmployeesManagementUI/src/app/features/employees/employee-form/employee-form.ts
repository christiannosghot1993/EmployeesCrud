import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { EmployeeRequest } from '../../../core/models/employee.model';
import { EmployeeService } from '../../../core/services/employee.service';

@Component({
  selector: 'app-employee-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './employee-form.html',
})
export class EmployeeForm {
  private readonly fb = inject(FormBuilder);
  private readonly service = inject(EmployeeService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  private readonly employeeId = this.route.snapshot.paramMap.get('id');

  protected readonly isEdit = signal(this.employeeId !== null);
  protected readonly loading = signal(false);
  protected readonly error = signal<string | null>(null);

  protected readonly form = this.fb.nonNullable.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    position: ['', Validators.required],
    hireDate: ['', Validators.required],
  });

  constructor() {
    if (this.employeeId) {
      this.service.getById(this.employeeId).subscribe({
        next: (employee) =>
          this.form.patchValue({
            firstName: employee.firstName,
            lastName: employee.lastName,
            email: employee.email,
            position: employee.position,
            hireDate: employee.hireDate.substring(0, 10),
          }),
        error: () => this.error.set('Unable to load the employee.'),
      });
    }
  }

  protected submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);
    const payload: EmployeeRequest = this.form.getRawValue();

    const request$ = this.employeeId
      ? this.service.update(this.employeeId, payload)
      : this.service.create(payload);

    request$.subscribe({
      next: () => this.router.navigate(['/employees']),
      error: (err) => {
        this.error.set(err?.error?.detail ?? 'Unable to save the employee.');
        this.loading.set(false);
      },
    });
  }
}
