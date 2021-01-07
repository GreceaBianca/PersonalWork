import { SelfUnsubscriberBase } from './SelfUnsubscriberBase';

export abstract class LoadableComponentBase extends SelfUnsubscriberBase {
  private _isLoading: boolean;

  constructor() {
    super();
  }

  public get isLoading() {
    return this._isLoading;
  }

  protected startLoader() {
    this._isLoading = true;
  }

  protected stopLoader() {
    this._isLoading = false;
  }
}