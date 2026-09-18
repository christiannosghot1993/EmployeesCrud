import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { AuthService } from '../services/auth.service';
import { authInterceptor } from './auth.interceptor';

describe('authInterceptor', () => {
  function configure(token: string | null, logout = () => {}) {
    const authStub = { getToken: () => token, logout } as Pick<AuthService, 'getToken' | 'logout'>;
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(withInterceptors([authInterceptor])),
        provideHttpClientTesting(),
        { provide: AuthService, useValue: authStub },
      ],
    });
  }

  it('adds the Authorization header when a token exists', () => {
    configure('jwt-token');
    const http = TestBed.inject(HttpClient);
    const httpMock = TestBed.inject(HttpTestingController);

    http.get('/api/employees').subscribe();

    const req = httpMock.expectOne('/api/employees');
    expect(req.request.headers.get('Authorization')).toBe('Bearer jwt-token');
    req.flush([]);
    httpMock.verify();
  });

  it('calls logout on a 401 response', () => {
    const logout = vi.fn();
    configure('jwt-token', logout);
    const http = TestBed.inject(HttpClient);
    const httpMock = TestBed.inject(HttpTestingController);

    http.get('/api/employees').subscribe({ error: () => {} });

    httpMock.expectOne('/api/employees').flush(null, { status: 401, statusText: 'Unauthorized' });
    expect(logout).toHaveBeenCalled();
    httpMock.verify();
  });
});
