Imports Microsoft.VisualBasic

#Region "Code 2"
Public Class Address
    Public Property city As Object
    Public Property country As String
    Public Property line1 As Object
    Public Property line2 As Object
    Public Property postal_code As String
    Public Property state As Object
End Class

Public Class AfterpayClearpay
    Public Property capture_method As Object
    Public Property reference As Object
    Public Property setup_future_usage As Object
End Class

Public Class AutomaticPaymentMethods
    Public Property enabled As Boolean
End Class

Public Class BillingDetails
    Public Property address As Address
    Public Property email As Object
    Public Property name As Object
    Public Property phone As Object
End Class

Public Class Card
    Public Property brand As String
    Public Property checks As Checks
    Public Property country As String
    Public Property description As Object
    Public Property exp_month As Integer
    Public Property exp_year As Integer
    Public Property fingerprint As String
    Public Property funding As String
    Public Property iin As Object
    Public Property installments As Object
    Public Property issuer As Object
    Public Property last4 As String
    Public Property mandate As Object
    Public Property moto As Object
    Public Property network As String
    Public Property three_d_secure As Object
    Public Property wallet As Object
    Public Property capture_method As Object
    Public Property mandate_options As Object
    Public Property request_three_d_secure As String
    Public Property setup_future_usage As Object
End Class

Public Class Charges
    Public Property _object As String
    Public Property data As List(Of Datum)
    Public Property has_more As Boolean
    Public Property url As String
End Class

Public Class Checks
    Public Property address_line1_check As Object
    Public Property address_postal_code_check As String
    Public Property cvc_check As String
End Class

Public Class Datum
    Public Property id As String
    Public Property _object As String
    Public Property amount As Integer
    Public Property amount_captured As Integer
    Public Property amount_refunded As Integer
    Public Property application As Object
    Public Property application_fee As Object
    Public Property application_fee_amount As Object
    Public Property authorization_code As Object
    Public Property balance_transaction As String
    Public Property billing_details As BillingDetails
    Public Property calculated_statement_descriptor As String
    Public Property captured As Boolean
    Public Property created As Integer
    Public Property currency As String
    Public Property customer As Object
    Public Property description As Object
    Public Property destination As Object
    Public Property dispute As Object
    Public Property disputed As Boolean
    Public Property failure_balance_transaction As Object
    Public Property failure_code As Object
    Public Property failure_message As Object
    Public Property fraud_details As FraudDetails
    Public Property invoice As Object
    Public Property level3 As Object
    Public Property livemode As Boolean
    Public Property metadata As Metadata
    Public Property on_behalf_of As Object
    Public Property order As Object
    Public Property outcome As Outcome
    Public Property paid As Boolean
    Public Property payment_intent As String
    Public Property payment_method As String
    Public Property payment_method_details As PaymentMethodDetails
    Public Property receipt_email As Object
    Public Property receipt_number As Object
    Public Property receipt_url As String
    Public Property refunded As Boolean
    Public Property refunds As Refunds
    Public Property review As Object
    Public Property shipping As Object
    Public Property source As Object
    Public Property source_transfer As Object
    Public Property statement_descriptor As Object
    Public Property statement_descriptor_suffix As Object
    Public Property status As String
    Public Property transfer As Object
    Public Property transfer_data As Object
    Public Property transfer_group As Object
End Class

Public Class FraudDetails
    Public Property stripe_report As Object
    Public Property user_report As Object
End Class

Public Class Metadata
End Class

Public Class Outcome
    Public Property network_status As String
    Public Property reason As Object
    Public Property risk_level As String
    Public Property risk_score As Integer
    Public Property rule As Object
    Public Property seller_message As String
    Public Property type As String
End Class

Public Class PaymentMethodDetails
    Public Property ach_credit_transfer As Object
    Public Property ach_debit As Object
    Public Property acss_debit As Object
    Public Property afterpay_clearpay As Object
    Public Property alipay As Object
    Public Property au_becs_debit As Object
    Public Property bacs_debit As Object
    Public Property bancontact As Object
    Public Property boleto As Object
    Public Property card As Card
    Public Property card_present As Object
    Public Property eps As Object
    Public Property fpx As Object
    Public Property giropay As Object
    Public Property grabpay As Object
    Public Property ideal As Object
    Public Property interac_present As Object
    Public Property klarna As Object
    Public Property konbini As Object
    Public Property multibanco As Object
    Public Property oxxo As Object
    Public Property p24 As Object
    Public Property paynow As Object
    Public Property sepa_debit As Object
    Public Property sofort As Object
    Public Property stripe_account As Object
    Public Property type As String
    Public Property us_bank_account As Object
    Public Property wechat As Object
    Public Property wechat_pay As Object
End Class

Public Class PaymentMethodOptions
    Public Property acss_debit As Object
    Public Property afterpay_clearpay As AfterpayClearpay
    Public Property alipay As Object
    Public Property au_becs_debit As Object
    Public Property bacs_debit As Object
    Public Property bancontact As Object
    Public Property boleto As Object
    Public Property card As Card
    Public Property card_present As Object
    Public Property eps As Object
    Public Property fpx As Object
    Public Property giropay As Object
    Public Property grabpay As Object
    Public Property ideal As Object
    Public Property interac_present As Object
    Public Property klarna As Object
    Public Property konbini As Object
    Public Property oxxo As Object
    Public Property p24 As Object
    Public Property paynow As Object
    Public Property sepa_debit As Object
    Public Property sofort As Object
    Public Property us_bank_account As Object
    Public Property wechat_pay As Object
End Class

Public Class Refunds
    Public Property _object As String
    Public Property data As List(Of Object)
    Public Property has_more As Boolean
    Public Property url As String
End Class

Public Class PaymentIntentRS
    Public Property id As String
    Public Property _object As String
    Public Property amount As Integer
    Public Property amount_capturable As Integer
    Public Property amount_received As Integer
    Public Property application As Object
    Public Property application_fee_amount As Object
    Public Property automatic_payment_methods As AutomaticPaymentMethods
    Public Property canceled_at As Object
    Public Property cancellation_reason As Object
    Public Property capture_method As String
    Public Property charges As Charges
    Public Property client_secret As String
    Public Property confirmation_method As String
    Public Property created As Integer
    Public Property currency As String
    Public Property customer As Object
    Public Property description As Object
    Public Property invoice As Object
    Public Property last_payment_error As Object
    Public Property livemode As Boolean
    Public Property metadata As Metadata
    Public Property next_action As Object
    Public Property on_behalf_of As Object
    Public Property payment_method As String
    Public Property payment_method_options As PaymentMethodOptions
    Public Property payment_method_types As List(Of String)
    Public Property processing As Object
    Public Property receipt_email As Object
    Public Property review As Object
    Public Property setup_future_usage As Object
    Public Property shipping As Object
    Public Property source As Object
    Public Property statement_descriptor As Object
    Public Property statement_descriptor_suffix As Object
    Public Property status As String
    Public Property transfer_data As Object
    Public Property transfer_group As Object
End Class

#End Region

#Region "Commnent 2"
'Public Class PaymentIntentRS

'    Public Property id As String
'    Public Property _object As String
'    Public Property amount As Integer
'    Public Property amount_capturable As Integer
'    Public Property amount_received As Integer
'    Public Property application As Object
'    Public Property application_fee_amount As Object
'    Public Property automatic_payment_methods As Automatic_Payment_Methods
'    Public Property canceled_at As Object
'    Public Property cancellation_reason As Object
'    Public Property capture_method As String
'    Public Property charges As Charges
'    Public Property client_secret As String
'    Public Property confirmation_method As String
'    Public Property created As Integer
'    Public Property currency As String
'    Public Property customer As Object
'    Public Property description As Object
'    Public Property invoice As Object
'    Public Property last_payment_error As Object
'    Public Property livemode As Boolean
'    Public Property metadata As Metadata1
'    Public Property next_action As Object
'    Public Property on_behalf_of As Object
'    Public Property payment_method As String
'    Public Property payment_method_options As Payment_Method_Options
'    Public Property payment_method_types() As String
'    Public Property processing As Object
'    Public Property receipt_email As Object
'    Public Property review As Object
'    Public Property setup_future_usage As Object
'    Public Property shipping As Object
'    Public Property source As Object
'    Public Property statement_descriptor As Object
'    Public Property statement_descriptor_suffix As Object
'    Public Property status As String
'    Public Property transfer_data As Object
'    Public Property transfer_group As Object
'End Class

'Public Class Automatic_Payment_Methods
'    Public Property enabled As Boolean
'End Class

'Public Class Charges
'    Public Property _object As String
'    Public Property data() As Datum
'    Public Property has_more As Boolean
'    Public Property url As String
'End Class

'Public Class Datum
'    Public Property id As String
'    Public Property _object As String
'    Public Property amount As Integer
'    Public Property amount_captured As Integer
'    Public Property amount_refunded As Integer
'    Public Property application As Object
'    Public Property application_fee As Object
'    Public Property application_fee_amount As Object
'    Public Property authorization_code As Object
'    Public Property balance_transaction As String
'    Public Property billing_details As Billing_Details
'    Public Property calculated_statement_descriptor As String
'    Public Property captured As Boolean
'    Public Property created As Integer
'    Public Property currency As String
'    Public Property customer As Object
'    Public Property description As Object
'    Public Property destination As Object
'    Public Property dispute As Object
'    Public Property disputed As Boolean
'    Public Property failure_balance_transaction As Object
'    Public Property failure_code As Object
'    Public Property failure_message As Object
'    Public Property fraud_details As Fraud_Details
'    Public Property invoice As Object
'    Public Property level3 As Object
'    Public Property livemode As Boolean
'    Public Property metadata As Metadata
'    Public Property on_behalf_of As Object
'    Public Property order As Object
'    Public Property outcome As Outcome
'    Public Property paid As Boolean
'    Public Property payment_intent As String
'    Public Property payment_method As String
'    Public Property payment_method_details As Payment_Method_Details
'    Public Property receipt_email As Object
'    Public Property receipt_number As Object
'    Public Property receipt_url As String
'    Public Property refunded As Boolean
'    Public Property refunds As Refunds
'    Public Property review As Object
'    Public Property shipping As Object
'    Public Property source As Object
'    Public Property source_transfer As Object
'    Public Property statement_descriptor As Object
'    Public Property statement_descriptor_suffix As Object
'    Public Property status As String
'    Public Property transfer As Object
'    Public Property transfer_data As Object
'    Public Property transfer_group As Object
'End Class

'Public Class Billing_Details
'    Public Property address As Address
'    Public Property email As Object
'    Public Property name As Object
'    Public Property phone As Object
'End Class

'Public Class Address
'    Public Property city As Object
'    Public Property country As String
'    Public Property line1 As Object
'    Public Property line2 As Object
'    Public Property postal_code As String
'    Public Property state As Object
'End Class

'Public Class Fraud_Details
'    Public Property stripe_report As Object
'    Public Property user_report As Object
'End Class

'Public Class Metadata
'End Class

'Public Class Outcome
'    Public Property network_status As String
'    Public Property reason As Object
'    Public Property risk_level As String
'    Public Property risk_score As Integer
'    Public Property rule As Object
'    Public Property seller_message As String
'    Public Property type As String
'End Class

'Public Class Payment_Method_Details
'    Public Property ach_credit_transfer As Object
'    Public Property ach_debit As Object
'    Public Property acss_debit As Object
'    Public Property afterpay_clearpay As Object
'    Public Property alipay As Object
'    Public Property au_becs_debit As Object
'    Public Property bacs_debit As Object
'    Public Property bancontact As Object
'    Public Property boleto As Object
'    Public Property card As Card
'    Public Property card_present As Object
'    Public Property eps As Object
'    Public Property fpx As Object
'    Public Property giropay As Object
'    Public Property grabpay As Object
'    Public Property ideal As Object
'    Public Property interac_present As Object
'    Public Property klarna As Object
'    Public Property konbini As Object
'    Public Property multibanco As Object
'    Public Property oxxo As Object
'    Public Property p24 As Object
'    Public Property paynow As Object
'    Public Property sepa_debit As Object
'    Public Property sofort As Object
'    Public Property stripe_account As Object
'    Public Property type As String
'    Public Property us_bank_account As Object
'    Public Property wechat As Object
'    Public Property wechat_pay As Object
'End Class

'Public Class Card
'    Public Property brand As String
'    Public Property checks As Checks
'    Public Property country As String
'    Public Property description As Object
'    Public Property exp_month As Integer
'    Public Property exp_year As Integer
'    Public Property fingerprint As String
'    Public Property funding As String
'    Public Property iin As Object
'    Public Property installments As Object
'    Public Property issuer As Object
'    Public Property last4 As String
'    Public Property mandate As Object
'    Public Property moto As Object
'    Public Property network As String
'    Public Property three_d_secure As Object
'    Public Property wallet As Object
'End Class

'Public Class Checks
'    Public Property address_line1_check As Object
'    Public Property address_postal_code_check As String
'    Public Property cvc_check As String
'End Class

'Public Class Refunds
'    Public Property _object As String
'    Public Property data() As Object
'    Public Property has_more As Boolean
'    Public Property url As String
'End Class

'Public Class Metadata1
'End Class

'Public Class Payment_Method_Options
'    Public Property acss_debit As Object
'    Public Property afterpay_clearpay As Afterpay_Clearpay
'    Public Property alipay As Object
'    Public Property au_becs_debit As Object
'    Public Property bacs_debit As Object
'    Public Property bancontact As Object
'    Public Property boleto As Object
'    Public Property card As Card1
'    Public Property card_present As Object
'    Public Property eps As Object
'    Public Property fpx As Object
'    Public Property giropay As Object
'    Public Property grabpay As Object
'    Public Property ideal As Object
'    Public Property interac_present As Object
'    Public Property klarna As Object
'    Public Property konbini As Object
'    Public Property oxxo As Object
'    Public Property p24 As Object
'    Public Property paynow As Object
'    Public Property sepa_debit As Object
'    Public Property sofort As Object
'    Public Property us_bank_account As Object
'    Public Property wechat_pay As Object
'End Class

'Public Class Afterpay_Clearpay
'    Public Property capture_method As Object
'    Public Property reference As Object
'    Public Property setup_future_usage As Object
'End Class

'Public Class Card1
'    Public Property capture_method As Object
'    Public Property installments As Object
'    Public Property mandate_options As Object
'    Public Property network As Object
'    Public Property request_three_d_secure As String
'    Public Property setup_future_usage As Object
'End Class


#End Region
#Region "commented 1"
'Public Class PaymentIntentRS

'    Public Property id As String
'    Public Property _object As String
'    Public Property amount As Integer
'    Public Property amount_capturable As Integer
'    Public Property amount_details As Amount_Details
'    Public Property amount_received As Integer
'    Public Property application As Object
'    Public Property application_fee_amount As Object
'    Public Property automatic_payment_methods As Object
'    Public Property canceled_at As Object
'    Public Property cancellation_reason As Object
'    Public Property capture_method As String
'    Public Property charges As Charges
'    Public Property client_secret As String
'    Public Property confirmation_method As String
'    Public Property created As Integer
'    Public Property currency As String
'    Public Property customer As Object
'    Public Property description As String
'    Public Property invoice As Object
'    Public Property last_payment_error As Object
'    Public Property livemode As Boolean
'    Public Property metadata As Metadata
'    Public Property next_action As Object
'    Public Property on_behalf_of As Object
'    Public Property payment_method As Object
'    Public Property payment_method_options As Payment_Method_Options
'    Public Property payment_method_types() As String
'    Public Property processing As Object
'    Public Property receipt_email As Object
'    Public Property review As Object
'    Public Property setup_future_usage As Object
'    Public Property shipping As Object
'    Public Property statement_descriptor As Object
'    Public Property statement_descriptor_suffix As Object
'    Public Property status As String
'    Public Property transfer_data As Object
'    Public Property transfer_group As Object
'End Class

'Public Class Amount_Details
'    Public Property tip As Tip
'End Class

'Public Class Tip
'End Class

'Public Class Charges
'    Public Property _object As String
'    Public Property data() As Object
'    Public Property has_more As Boolean
'    Public Property url As String
'End Class

'Public Class Metadata
'End Class

'Public Class Payment_Method_Options
'    Public Property card As Card
'End Class

'Public Class Card
'    Public Property installments As Object
'    Public Property mandate_options As Object
'    Public Property network As Object
'    Public Property request_three_d_secure As String
'End Class
#End Region


