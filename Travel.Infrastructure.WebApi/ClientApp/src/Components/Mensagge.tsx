import Swal from 'sweetalert2'
import 'sweetalert2/src/sweetalert2.scss'
export const Mensagge = (msg: string, text: string, type: 'error' | 'success') => {
    Swal.fire({
        title: msg,
        text,
        icon: type
    });
}