import { TestBed } from '@angular/core/testing';

import { ServCine } from './serv-cine';

describe('ServCine', () => {
  let service: ServCine;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServCine);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
