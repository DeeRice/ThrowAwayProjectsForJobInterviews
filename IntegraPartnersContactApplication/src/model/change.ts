export class Change<T> {
    type!: 'insert' | 'update' | 'remove';
  
    key!: string;
  
    data!: Partial<T>;
  }
  