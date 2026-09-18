import { TestBed } from '@angular/core/testing';
import { provideRouter, Router, UrlTree } from '@angular/router';
import { authGuard } from './auth.guard';
import { AuthService } from '../services/auth.service';

describe('authGuard', () => {
  function run(isAuthenticated: boolean) {
    const authStub = { isAuthenticated: () => isAuthenticated } as Pick<AuthService, 'isAuthenticated'>;
    TestBed.configureTestingModule({
      providers: [
        { provide: AuthService, useValue: authStub },
        provideRouter([]),
      ],
    });
    return TestBed.runInInjectionContext(() => authGuard({} as never, {} as never));
  }

  it('allows access when authenticated', () => {
    expect(run(true)).toBe(true);
  });

  it('redirects to /login when not authenticated', () => {
    const result = run(false);
    const router = TestBed.inject(Router);
    expect(result).toBeInstanceOf(UrlTree);
    expect(result).toEqual(router.createUrlTree(['/login']));
  });
});
