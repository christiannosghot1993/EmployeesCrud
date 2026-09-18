export interface Employee {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  position: string;
  hireDate: string;
}

export interface EmployeeRequest {
  firstName: string;
  lastName: string;
  email: string;
  position: string;
  hireDate: string;
}
