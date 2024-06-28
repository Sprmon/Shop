import React from "react";

interface IErrorBoundaryState {
  hasError: boolean;
  error: Error | null;
  message: React.ErrorInfo | null;
}

export class ErrorBoundary extends React.Component<any, IErrorBoundaryState> {
  constructor(props: any) {
    super(props);
    this.state = { hasError: false, error: null, message: null };
  }

  componentDidCatch(error: Error, message: React.ErrorInfo) {
    // Update state with error details
    this.setState({ hasError: true, error, message });
  }

  clearAndSaveData() {
    try {
      // useSyncStore.getState().export();
    } finally {
      localStorage.clear();
      location.reload();
    }
  }

  render(): React.ReactNode {
    if (this.state.hasError) {
      return (
        <div className="error-ui">
          An unhandled error has occurred.
          <a href="script: " className="reload">Reload</a>
          <a className="dismiss">🗙</a>
        </div>
      )
    }
    return this.props.children;
  }
}
