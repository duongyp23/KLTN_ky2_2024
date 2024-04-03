
const Resource = {
    ErrorCode: {
        /// Lỗi do exception chưa xác định đưọc
        Exception: 1,
        /// Lỗi do validate dữ liệu thất bại
        Validate:2,
        /// Lỗi do trùng mã
        DuplicateCode: 3
    },
    ErrorMessage: {
        ErrorMessageAdd: "Thêm dữ liệu không thành công",
        ErrorMessageFix: "Lưu dữ liệu không thành công",
        ErrorMessageDelete: "Xóa dữ liệu không thành công",
        ErrorMessageGetProperty : "Không tìm thấy dữ liệu"
    },
    SuccessMessage: {
        SuccessMessageAdd: "Thêm mới dữ liệu thành công",
        SuccessMessageFix: "Lưu dữ liệu thành công",
        SuccessMessageDelete: "Xóa dữ liệu thành công",

    },
    PopupNotice: {
        ErrorGetPaging : "Lấy danh sách tài sản không thành công!",
        ErrorAttritionValue : "Giá trị hao mòn năm phải nhỏ hơn nguyên giá",
        ErrorAttritionRate: "Tỉ lệ hao mòn phải bằng 1/số năm sử dụng",
        ErrorDuplicate: "Mã tài sản đã tồn tại",
        ErrorGetDepartment: "Lấy danh sách phòng ban không thành công!",
        ErrorGetPropertyType: "Lấy danh sách loại tài sản không thành công!",
        ErrorGetBudget: "Lấy danh sách nguồn tài sản không thành công!",
        ErrorGetNewCode: "Lấy mã tài sản mới không thành công!",
        ErrorStartUsingDate: "Ngày sử dụng phải lớn hơn ngày mua",
        ErrorDate: "Ngày mua và ngày bắt đầu sử dụng phải nhỏ hơn hoặc bằng hiện tại",
        CancelFix: "Thông tin thay đổi sẽ không được cập nhật nếu bạn không lưu. Bạn có muốn lưu các thay đổi này?",
        ErrorGetVoucher: "Lấy thông tin ghi tanwg không thành công!",
        ErrorGetPropertyInVoucher: "Lấy danh sách tài sản của ghi tăng không thành công!",

    },
    DecimalCharacter: ',',
    DigitGroupSeparator : '.',
    IsActive: {
        // đã ghi tanwg
        Active: 1,
        // chưa ghi tăng
        NotActive: 0,
    },
    ProductStatus: [
        {
            id: 1,
            name: 'Chưa cho thuê'
        },{
            id: 2,
            name: 'Đang cho thuê'
        },{
            id: 3,
            name: 'Đã bán'
        },
    ]
}
export default Resource;