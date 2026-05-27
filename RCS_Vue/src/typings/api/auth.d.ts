declare namespace Api {
  /**
   * namespace Auth
   *
   * backend api module: "auth"
   */
  namespace Auth {
    interface LoginToken {
      access_token: string;
      expires_in: number;
      token_type: string;
      refresh_token: string;
      scope?: string;
    }

    interface UserInfo {
      userId: string;
      userName: string;
      roles: string[];
      policies: Record<string, boolean>;
    }
  }

  namespace Abp {
    interface OpenIddictErrorResponse {
      error: string;
      error_description?: string;
      error_uri?: string;
    }

    interface ValidationError {
      message: string;
      members?: string[];
    }

    interface ErrorInfo {
      code?: string;
      message: string;
      details?: string;
      data?: Record<string, unknown>;
      validationErrors?: ValidationError[];
    }

    interface ErrorResponse {
      error: ErrorInfo;
    }

    interface CurrentUser {
      isAuthenticated: boolean;
      id?: string;
      tenantId?: string;
      userName?: string;
      name?: string;
      surName?: string;
      email?: string;
      emailVerified?: boolean;
      phoneNumber?: string;
      phoneNumberVerified?: boolean;
      roles?: string[];
    }

    interface ApplicationConfiguration {
      auth: {
        grantedPolicies: Record<string, boolean>;
      };
      currentUser: CurrentUser;
      [key: string]: unknown;
    }
  }
}
