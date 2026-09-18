import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { Router, provideRouter } from '@angular/router';
import { environment } from '../../../environments/environment';
import { AuthResponse } from '../models/auth.model';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  const authResponse: AuthResponse = {
    token: 'jwt-token',
    expiresAtUtc: new Date().toISOString(),
    userId: '00000000-0000-0000-0000-000000000001',
    username: 'admin',
    email: 'admin@example.com',
  };

  beforeEach(() => {
    localStorage.clear();
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
    vi.spyOn(TestBed.inject(Router), 'navigate').mockResolvedValue(true);
  });

  afterEach(() => httpMock.verify());

  it('stores the token and username after login', () => {
    service.login({ username: 'admin', password: 'Admin123!' }).subscribe();

    const req = httpMock.expectOne(`${environment.apiBaseUrl}/auth/login`);
    expect(req.request.method).toBe('POST');
    req.flush(authResponse);

    expect(service.getToken()).toBe('jwt-token');
    expect(service.username()).toBe('admin');
    expect(service.isAuthenticated()).toBe(true);
  });

  it('clears the session on logout', () => {
    service.login({ username: 'admin', password: 'Admin123!' }).subscribe();
    httpMock.expectOne(`${environment.apiBaseUrl}/auth/login`).flush(authResponse);

    service.logout();

    expect(service.getToken()).toBeNull();
    expect(service.isAuthenticated()).toBe(false);
    expect(localStorage.getItem('ems_token')).toBeNull();
  });
});
