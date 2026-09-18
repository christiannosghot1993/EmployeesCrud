import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from '../../../environments/environment';
import { Employee, EmployeeRequest } from '../models/employee.model';
import { EmployeeService } from './employee.service';

describe('EmployeeService', () => {
  let service: EmployeeService;
  let httpMock: HttpTestingController;
  const baseUrl = `${environment.apiBaseUrl}/employees`;

  const employee: Employee = {
    id: '00000000-0000-0000-0000-000000000001',
    firstName: 'Grace',
    lastName: 'Hopper',
    email: 'grace@example.com',
    position: 'Admiral',
    hireDate: '1943-01-01T00:00:00',
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    service = TestBed.inject(EmployeeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpMock.verify());

  it('fetches all employees', () => {
    let result: Employee[] = [];
    service.getAll().subscribe((data) => (result = data));

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('GET');
    req.flush([employee]);

    expect(result).toHaveLength(1);
    expect(result[0].email).toBe('grace@example.com');
  });

  it('creates an employee', () => {
    const payload: EmployeeRequest = {
      firstName: 'Grace',
      lastName: 'Hopper',
      email: 'grace@example.com',
      position: 'Admiral',
      hireDate: '1943-01-01',
    };

    service.create(payload).subscribe();

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(payload);
    req.flush(employee);
  });

});
