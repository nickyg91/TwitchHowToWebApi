import axios, { AxiosError } from 'axios';
const axiosInstance = axios.create({
  baseURL: '/'
});
function setupInstance(): void {
  axiosInstance.interceptors.request.use((instance) => {
    return instance;
  });

  axiosInstance.interceptors.response.use(
    (response) => {
      return response;
    },
    (error: AxiosError) => {
      if (error.response?.status === 500) {
        // fire error here
      }
      if (error.response?.status === 400) {
        const modelData = error.response?.data as ModelStateErrors;
        const errorMessage = determine400ErrorMessage(modelData);
      }
      return Promise.reject(error);
    }
  );
}

function determineErrorMessage(trace: string): string {
  if (trace.length < 1) {
    return 'An error occurred';
  }
  return trace.split('\n')[0].split(':')[1];
}

function determine400ErrorMessage(data: ModelStateErrors): string {
  let invalidFields = '';
  for (const [key] of Object.entries(data.errors)) {
    let errorStr = `${key}: `;
    const errorMessages = data.errors[key];
    errorMessages?.forEach((val) => {
      errorStr += `${val}\n`;
    });
    invalidFields += errorStr;
  }
  return invalidFields;
}

function setToken(token: string): void {
  axiosInstance.interceptors.request.use((instance) => {
    instance.headers.set('Authorization', `Bearer ${token}`);
    return instance;
  });
}

function refreshToken(refreshToken: string): void {
  // stubbed for now
}

export class ModelStateErrors {
  title!: string;
  errors!: {
    [key: string]: Array<string>;
  };
}

export { axiosInstance, setupInstance, setToken, refreshToken };
